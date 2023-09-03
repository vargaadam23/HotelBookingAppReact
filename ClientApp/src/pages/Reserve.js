import React, { Component } from 'react';
import { executeGetRoom } from '../data/ApiRequest.js';
import { Room, APIPaths } from '../data/ApiConstants.js';
import { FacilityBadge } from '../components/room-list-item/FacilityBadge.js';
import { FormGroup, Label, Input, Button, Spinner } from 'reactstrap';
import { ReserveSummary } from '../components/reserve-components/ReserveSummary.js';
import { Loader } from '../components/loader/Loader.js';

export class Reserve extends Component {
    static displayName = Reserve.name;

    constructor() {
        super();

        this.state = {
            room: [],
            checkIn: '',
            checkOut: '',
            roomId: '',
            isSubmitLoading: false
        };
    }

    componentDidMount() {
        executeGetRoom(this.getSearchParam(), this.setRoom.bind(this), this.onFail.bind(this));
        this.setState({ roomId:this.getSearchParam() })
    }

    setRoom(room) {
        this.setState({ room });
        console.log(room);
    }

    onFail(e) {
        console.log(e, APIPaths);
    }

    getSearchParam() {
        const search = window.location.search;
        const params = new URLSearchParams(search);
        return params.get('roomId') || null;
    }

    onInputChange(e) {
        e.preventDefault();

        this.setState({ [e.target.name]: e.target.value }, () => console.log(this.state));
    }

    getDatesDifference() {
        const {
            checkIn,
            checkOut
        } = this.state;

        const checkInDate = new Date(checkIn);
        const checkOutDate = new Date(checkOut);
        const diffTime = Math.abs(checkOutDate - checkInDate);
        return Math.ceil(diffTime / (1000 * 60 * 60 * 24));
    }

    getTotalPrice() {
        const {
            room: {
                pricePerNight
            }
        } = this.state;

        const diffDays = this.getDatesDifference();

        return Number(pricePerNight) * diffDays;
    }

    onSubmit(e) {
        const {
            checkIn,
            checkOut,
            roomId
        } = this.state;

        e.preventDefault();

        this.setState({ isSubmitLoading: true });

        const data = {
            checkIn,
            checkOut,
            roomId
        };

        const response = fetch("/api/rooms/reserve", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        }).then((response) => {
            return response.json()
        }).then((response) =>
            response
        ).catch((e) =>
            console.log(e)
        ).finally(() =>
            this.setState({ isSubmitLoading: false })    
        );

        console.log(response)
    }

    render() {
        const {
            room: {
                numberOfBeds,
                facilities = [],
                pricePerNight
            },
            checkIn,
            checkOut,
            isSubmitLoading
        } = this.state;

        const totalPrice = this.getTotalPrice();
        const diffDays = this.getDatesDifference();

        return (
            <div className="container">
                <div className="row">
                    <div className="col-4">
                        <img className="card-img-top placeholder" src="/placeholder.png" alt="Card cap" />
                    </div>
                    <div className="col-8">
                        <div className="row">
                            {`${numberOfBeds} Bed Room available from ${pricePerNight} EUR per night`}
                        </div>
                        <div className="row">
                            {facilities.map(facility => <FacilityBadge facility={facility} isSimple={false} />)}
                        </div>
                    </div>
                </div>
                <form onSubmit={this.onSubmit } className="row">
                    <div className="col-5">
                        <FormGroup>
                            <Label for="checkIn">
                                Check In Date and Time
                            </Label>
                            <Input
                                id="checkIn"
                                name="checkIn"
                                placeholder="date placeholder"
                                type="datetime-local"
                                required
                                onChange={this.onInputChange.bind(this)}
                                value={checkIn}
                            />
                        </FormGroup>
                    </div>
                    <div className="col-5">
                        <FormGroup>
                            <Label for="checkOut">
                                Check Out Date and Time
                            </Label>
                            <Input
                                id="checkOut"
                                name="checkOut"
                                placeholder="date placeholder"
                                type="datetime-local"
                                required
                                onChange={this.onInputChange.bind(this)}
                                value={checkOut}
                            />
                        </FormGroup>
                    </div>
                    <div className="col-2">
                        <ReserveSummary checkIn={checkIn} checkOut={checkOut} totalPrice={totalPrice} diffDays={diffDays} />
                    </div>
                    <div className="col-12">
                        <Button>Submit</Button>
                    </div>
                </form>
                <Loader isLoading={isSubmitLoading} />
            </div>
        );
    }
}
