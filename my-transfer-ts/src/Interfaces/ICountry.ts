export interface ICountry {
    id: number;
    name: string;
    code: string;
    slug: string;
    description: string;
    shortDescription: string;
    tags: string[];
    image: string | null;
}