import React, { Component } from 'react';
import {ReservationsTable } from './reservations/ReservationsTable'

export class Reservations extends Component {
    static displayName = Reservations.name;

    
    

  render() {
    return (
      <ReservationsTable/>
    );
  }
}
