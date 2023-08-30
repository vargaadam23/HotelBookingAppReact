import React, { Component } from 'react';
import { executeListRequest } from '../data/ApiRequest.js';
import { Room, APIPaths } from '../data/ApiConstants.js';
import { RoomListItem } from '../components/room-list-item/RoomListItem.js';
import { AuthorizeService } from '../components/api-authorization/AuthorizeService.js';

export class Rooms extends Component {
    static displayName = Rooms.name;

    constructor() {
        super();

        this.state = {
            rooms: []
        };
    }

    componentDidMount() {
        executeListRequest(Room, {}, this.setRooms.bind(this), this.onFail.bind(this));
        const authService = new AuthorizeService();
        console.log(authService.getAccessToken());
    }

    setRooms(rooms) {
        this.setState({ rooms });
        console.log(rooms);
    }

    onFail(e) {

        console.log(e, APIPaths);
    }

    render() {
        const {
            rooms
        } = this.state;
        console.log(rooms);
        return (
            <div className="row">
                {rooms.map(room => <RoomListItem room={room}/>)}
            </div>
        );
    }
}
