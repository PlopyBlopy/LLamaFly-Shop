import {
  Card,
  CardMedia,
  CardContent,
  Typography,
  Rating,
  Button,
  Grid,
  Container,
  Divider,
  Chip,
} from "@mui/material";
import styles from "./index.module.css";
import { Product } from "../../../shared/product-service-products";
import { observer } from "mobx-react-lite";
type Props = {
  product: Product;
};
export const ProductDetail = observer(({ product }: Props) => {
  if (!product) {
    return <div>Product not found</div>; // Обработка случая, когда product отсутствует
  }

  const { title, description, price, rating, sellerId, categoryId } = product;

  return (
    <Container className={styles.container}>
      <Card className={styles.card} elevation={0}>
        <Grid container spacing={4}>
          {/* Блок с изображением */}
          <Grid item xs={12} md={6}>
            <CardMedia
              component="img"
              image={product.image}
              alt={title}
              className={styles.image}
            />
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
              <Typography variant="h3" className={styles.price}>
                ${price.toFixed(0)}
              </Typography>

              {/* Кнопки */}
              <div className={styles.buttons}>
                <Button
                  variant="contained"
                  color="primary"
                  className={styles.cartButton}>
                  Добавить в корзину
                </Button>
              </div>

              {/* Продавец и категория */}
              <Divider className={styles.divider} />
              <div className={styles.chips}>
                <Chip label={`Продавец: #${sellerId}`} variant="outlined" />
              </div>
              <div className={styles.chips}>
                <Chip label={`Категория: #${categoryId}`} variant="outlined" />
              </div>

              {/* Описание товара */}
              <Divider className={styles.divider} />
              <Typography variant="body1" className={styles.description}>
                {description}
              </Typography>
            </CardContent>
          </Grid>
        </Grid>
      </Card>
    </Container>
  );
});
