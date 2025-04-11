export type Product = {
  id: string;
  // images: Map<string, string>;
  images: Map<number, string>;
  title: string;
  description: string;
  price: number;
  rating: number;
  categoryId: string;
  sellerId: string;
};

export type ProductCard = {
  id: string;
  image: string;
  title: string;
  price: number;
  rating: number;
};

export type ProductCreate = {
  title: string;
  description: string;
  price: number;
  categoryId: string;
  sellerId: string;
};

export type QueryParams = {
  search?: string;
  categoryId?: string;
  sortProp?: string;
  sortOrder?: string;
};

export type ProductCreateResponse = {
  productId: string;
};
