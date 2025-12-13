export default interface IUserRegister {
    firstName: string,
    lastName: string,
    email: string,
    password: string,
    image: File | null,
}