import React, { Component } from 'react';

export class ReserveSummary extends Component {
    static displayName = ReserveSummary.name;

    getNumberOfDays() {
        const {
            checkIn,
            checkOut,
            diffDays
        } = this.props;

        if (!checkIn || !checkOut || !diffDays) {
            return 'Choose dates';
        }


        return diffDays === 1 ? '1 Night' : `${diffDays} Nights`;
    }

    getTotalPrice() {
        const {
            totalPrice
        } = this.props;

        if (!totalPrice) {
            return null;
        }

        return `Total: ${totalPrice}`;
    }

    render() {
        const {
            name = '',
            checkIn,
            checkOut,
        } = this.props;
        return (
            <div className="container">
                <div className="row">
                    <h3>
                        Your reservation
                        <small class="text-body-secondary">{  name }</small>
                    </h3>
                </div>
                <div className="row">
                    {`${checkIn} - ${checkOut}`}
                </div>
                <div className="row">
                    {this.getNumberOfDays()}
                </div>
                <div className="row">
                    {this.getTotalPrice()}
                </div>
            </div>

        );
    }
}
