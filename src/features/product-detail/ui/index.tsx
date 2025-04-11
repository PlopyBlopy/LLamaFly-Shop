import { Card, CardContent, Typography, Rating, Button, Grid, Container, Divider } from "@mui/material";
import styles from "./index.module.css";
import { Product } from "../../../shared/services/product-service-products";
import { ImageGallery } from "../../image-gallery";
import Markdown from "react-markdown";
type Props = {
  product: Product;
};
export const ProductDetail = ({ product }: Props) => {
  if (!product) {
    return <div>Product not found</div>; // Обработка случая, когда product отсутствует
  }

  // const { title, description, price, rating, sellerId, categoryId } = product;
  const { title, description, price, rating } = product;

  return (
    <Container className={styles.container}>
      <Card className={styles.card} elevation={0}>
        <Grid container spacing={4}>
          {/* Блок с изображением */}
          <Grid item xs={12} md={6} className={styles.imageGalleryContainer}>
            <ImageGallery images={product.images} />
          </Grid>

          {/* Блок с информацией о товаре */}
          <Grid item xs={12} md={6}>
            <CardContent className={styles.content}>
              {/* Название товара */}
              <Typography variant="h4" component="h1" className={styles.title}>
                {title}
              </Typography>
              {/* Рейтинг */}
              <div className={styles.ratingContainer}>
                <Rating value={rating} precision={0.5} readOnly />
                <Typography variant="body2" className={styles.ratingText}>
                  {rating} / 5
                </Typography>
              </div>
              {/* Цена */}
              {/* TODO: Реализовать price как отдельная features */}
              <Typography variant="h3" className={styles.price}>
                {price.toFixed(0)} ₽
              </Typography>
              {/* Кнопки */}
              <div className={styles.buttons}>
                <Button variant="contained" color="primary" className={styles.cartButton}>
                  Добавить в корзину
                </Button>
              </div>
            </CardContent>
          </Grid>
          <Divider className={styles.divider} />
          <div className={styles.description}>
            <Markdown className={styles.description}>{description}</Markdown>
          </div>
        </Grid>
      </Card>
    </Container>
  );
};
