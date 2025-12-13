export interface UserTokenInfo {
    email: string;
    name: string;
    image: string;
    roles: string[] | string | undefined;
    exp: number;
}