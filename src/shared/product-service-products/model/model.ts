export type Product = {
    id: string;
    title: string;
    description: string;
    price: number;
    rating: number;
    categoryId: string;
    sellerId: string;
}

export type ProductCard = {
    id: string;
    title: string;
    price: number;
    rating: number;
}

export type QueryParams = {
    search?: string;
    categoryId?: string;
    sortProp?: string;
    sortOrder?: string;
}
