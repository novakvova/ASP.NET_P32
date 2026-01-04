export interface IRegisterModel {
    email: string;
    password: string;
    confirmPassword: string;
    firstName: string;
    lastName: string;
    phone: string;
    image: FileList | null;
}