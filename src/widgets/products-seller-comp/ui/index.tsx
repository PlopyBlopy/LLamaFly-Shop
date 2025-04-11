import { Typography } from "@mui/material";
import { ProductItemCard } from "../../../features/product-card";
import { Props } from "../config";
import styles from "./index.module.css";

export const ProductsSellerComp = ({
  productCardList,
  onProductDetailPage,
}: Props) => {
  return (
    <div className={styles.container}>
      <Typography component="h2" variant="h2" className={styles.header}>
        Мои продукты
      </Typography>
      <div className={styles.grid}>
        {productCardList?.map((product) => (
          <ProductItemCard
            key={product.id}
            card={product}
            onCardClick={onProductDetailPage}
          />
        ))}
      </div>
    </div>
  );
};
