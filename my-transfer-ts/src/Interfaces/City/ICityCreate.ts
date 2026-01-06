export interface ICityCreate {
    name: string;
    slug: string;
    description?: string;
    countryId: number | string;
    image?: File | null;
}