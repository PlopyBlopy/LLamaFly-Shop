export type Image = {
  order: number;
  image: File;
};

export type ImagesUpload = {
  productId: string;
  images: Image[];
};

export type ProductIds = {
  productIds: string[];
};
