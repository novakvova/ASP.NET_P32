export interface ICartItem {
    quantity: number,
    id: number,
    code: string,
    fromCityName: string,
    fromCountryName: string,
    toCityName: string,
    toCountryName: string,
    departureTime: string,
    arrivalTime: string,
    seatsTotal: number,
    seatsAvailable: number,
    statusName: string,
    price: string
}