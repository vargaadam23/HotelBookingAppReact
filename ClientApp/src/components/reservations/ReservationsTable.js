import React, { Component } from 'react';
import { executeListRequest } from '../data/ApiRequest.js';
import { Reservation } from '../../data/ApiConstants.js';
export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { reservations: [], loading: true };
    }

    componentDidMount() {
        this.getReservations()
    }

    renderReservationsTable() {
        const {
            reservations
        } = this.state;

        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Down</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {reservations.map(reservation =>
                        <tr key={reservation.id}>
                            <td>{reservation.checkIn}</td>
                            <td>{reservation.checkIn}</td>
                            <td>{reservation.checkIn}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    getReservations() {
        this.setState({ loading:true })
        const result = executeListRequest(Reservation);

        this.setState({ loading:false })

        console.log(result);

        this.setState({ reservations: result })
    }
}
