import React, { Component } from 'react';
import { NavItem } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class Footer extends Component {
    static displayName = Footer.name;

  render() {
      return (
          <footer className="Footer d-flex flex-wrap justify-content-between align-items-center py-3 my-4 border-top">
              <p className="col-md-4 mb-0 text-muted">© 2023 { this.displayName} Inc</p>

              <Link href="/" class="col-md-4 d-flex align-items-center justify-content-center mb-3 mb-md-0 me-md-auto Logo link-dark text-decoration-none">
                  HotelBookingApp
              </Link>

              <ul className="nav col-md-4 justify-content-end">
                  <NavItem className="nav-item"><Link href="#" className="nav-link px-2 text-muted">Rooms</Link></NavItem>
                  <NavItem className="nav-item"><Link href="#" className="nav-link px-2 text-muted">Reservations</Link></NavItem>
                  <NavItem className="nav-item"><Link href="#" className="nav-link px-2 text-muted">User profile</Link></NavItem>
                  <NavItem className="nav-item"><Link href="#" className="nav-link px-2 text-muted">Login</Link></NavItem>
                  <NavItem className="nav-item"><Link href="#" className="nav-link px-2 text-muted">Register</Link></NavItem>
              </ul>
          </footer>
    );
  }
}
