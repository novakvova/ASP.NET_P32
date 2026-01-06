import type {IAccountSearch} from "./IAccountSearch.ts";

export interface IAccountSearchResponse {
    items: IAccountSearch[];
    pagination: {
        totalCount: number;
        totalPages: number;
        itemsPerPage: number;
        currentPage: number;
    };
}