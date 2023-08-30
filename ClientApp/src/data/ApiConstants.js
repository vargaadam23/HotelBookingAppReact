export const Room = 'room';
export const Reservation = 'reservation';
export const Facility = 'facility';



export const RoomActions = {
    Reserve: 'reserve',
    List: 'list',
    Get: 'get'
};

export const ReservationActions = {
    List: 'list',
    Cancel: 'cancel',
    Update: 'update'
};

export const FacilityActions = {
    List: 'list'
};

const ReservationPrefix = '/api/reservations';

const RoomPrefix = '/api/rooms';

const FacilityPrefix = '/api/facilities';

export const RoomPaths = {
    ReserveRoom: `${RoomPrefix}/${RoomActions.Reserve}`,
    ListRoom: `${RoomPrefix}/${RoomActions.List}`,
    GetRoom: `${RoomPrefix}/${RoomActions.Get}`
};

export const ReservationPaths = {
    ListReservations: `${ReservationPrefix}/${ReservationActions.List}`,
    CancelReservation: `${ReservationPrefix}/${ReservationActions.Cancel}`,
    UpdateReservation: `${ReservationPrefix}/${ReservationActions.Update}`
};

export const FacilityPaths = {
    ListFacilities: `${FacilityPrefix}/${FacilityActions.List}`
};

export const ListActions = {
    [Room]: RoomPaths.ListRoom,
    [Facility]: FacilityPaths.ListFacilities,
    [Reservation]: ReservationPaths.ListReservations
}

export const APIPaths = {
    ...Object.values(ReservationPaths),
    ...Object.values(RoomPaths),
    ...Object.values(FacilityPaths)
}
