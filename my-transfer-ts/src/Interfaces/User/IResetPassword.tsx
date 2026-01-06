export interface IResetPassword {
    email: string;
    token: string;
    newPassword: string;
    confirmPassword: string;
}