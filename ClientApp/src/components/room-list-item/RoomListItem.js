import React, { Component } from 'react';
import { Link } from 'react-router-dom';
import { FacilityBadge } from './FacilityBadge';

export class RoomListItem extends Component {
    static displayName = RoomListItem.name;

    render() {
        const {
            room: {
                numberOfBeds,
                pricePerNight,
                facilities = [{ name: 'WiFi' }],
                roomNumber
            } = {}
        } = this.props;

        return (
            <div className="col-4">
                <div className="card text-center">
                    <img className="card-img-top placeholder" src="/placeholder.png" alt="Card cap" />
                    <div className="card-body">
                        <h5 className="card-title">{`${numberOfBeds} Bed Room`}</h5>
                        <h6 class="card-subtitle mb-2 text-muted">{`${pricePerNight} Per Night`}</h6>
                        {facilities.map(facility => <FacilityBadge facility={facility} />)}
                        <Link to={`/reserve?roomId=${roomNumber}`} className="btn btn-primary">Reserve!</Link>
                    </div>
                </div>
            </div>

        );
    }
}
