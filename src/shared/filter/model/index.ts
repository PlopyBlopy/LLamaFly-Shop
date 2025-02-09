export type ProductFilter = {
    id: string;
    title: string;
    param: string;
};

export type FilterParams = {
    search: string;
    categoryId: string;
    sortProp: string;
    sortOrder: string;
}