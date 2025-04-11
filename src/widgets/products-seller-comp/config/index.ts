import { ProductCard } from "../../../shared/services/product-service-products";

export type Props = {
  productCardList: ProductCard[];
  onProductDetailPage: (productTitle: string, id: string) => void;
};
