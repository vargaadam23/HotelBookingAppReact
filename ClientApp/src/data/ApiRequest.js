import authService from '../components/api-authorization/AuthorizeService';
import { ListActions, RoomPaths } from './ApiConstants.js';

export const executeRequest = (url, options = {}, onSuccess = () => { }, onFail = () => { }) => {
    const token = authService.getAccessToken().then(result => result);

    return fetch(url, {
        headers: !token ? {} : { 'Authorization': `Bearer ${token}` },
        ...options
    }).then(response => response.json()).then(response => onSuccess(response)).catch((e) => {
        onFail(e);
        return [];
    });;


}

export const executeListRequest = async (type, options = {}, onSuccess = () => { }, onFail = () => { }) => {
    if (ListActions[type]) {
        return executeRequest(ListActions[type], options, onSuccess, onFail);
    }

    return [];
}

export const executeGetRoom = async (roomId, onSuccess = () => { }, onFail = () => { }) => {
    if (!roomId) {
        return null;
    }
    
     return executeRequest(`${RoomPaths.GetRoom}?${new URLSearchParams({roomId})}`, {}, onSuccess, onFail);
}

export const executeReserve = async (roomId, onSuccess = () => { }, onFail = () => { }) => {
    if (!roomId) {
        return null;
    }

    return executeRequest(`${RoomPaths.GetRoom}?${new URLSearchParams({ roomId })}`, {}, onSuccess, onFail);
}

export const executeGetReservations = async (roomId, onSuccess = () => { }, onFail = () => { }) => {
    if (!roomId) {
        return null;
    }

    return executeRequest(`${RoomPaths.GetRoom}?${new URLSearchParams({ roomId })}`, {}, onSuccess, onFail);
}


