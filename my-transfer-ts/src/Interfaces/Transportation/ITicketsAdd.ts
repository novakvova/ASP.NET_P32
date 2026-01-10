export interface ITicketsAdd {
    code: string,
    fromCityId: number,
    toCityId: number,
    departureTime: string,
    arrivalTime: string,
    seatsTotal: number,
    seatsAvailable: number,
    statusId: number,
    price: number
}