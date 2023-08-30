import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class FetchData {
    static displayName = FetchData.name;

    async populateWeatherData() {
        const token = await authService.getAccessToken();
        const response = await fetch('weatherforecast', {
            headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
        });
        const data = await response.json();
        this.setState({ forecasts: data, loading: false });
    }
}

const executeRequest = async (url, options = {}) => {
    const token = await authService.getAccessToken();
    const response = await fetch('weatherforecast', {
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
        ...options
    });

    return await response.json();
}

export const getRooms = () => {

}


