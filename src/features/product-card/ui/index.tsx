import styles from "./index.module.css";
import {
  Card,
  CardActionArea,
  CardContent,
  CardMedia,
  Typography,
} from "@mui/material";
import { ProductCard } from "../../../shared/services/product-service-products";

type Props = {
  card: ProductCard;
  onCardClick: (title: string, id: string) => void;
};

export const ProductItemCard = ({ card, onCardClick }: Props) => {
  const MAX_TITLE_LENGTH = 70;

  const formatPrice = (price: number) => {
    return price.toLocaleString("ru-RU") + " ₽";
  };

  const truncateTitle = (text: string) => {
    return text.length > MAX_TITLE_LENGTH
      ? text.slice(0, MAX_TITLE_LENGTH) + "..."
      : text;
  };

  return (
    <Card className={styles.card}>
      <CardActionArea
        onClick={() => onCardClick(card.title, card.id)}
        className={styles.cardAction}>
        <div className={styles.imageContainer}>
          <CardMedia
            component="img"
            image={card.image}
            alt={card.title}
            className={styles.image}
          />
        </div>
        <CardContent className={styles.cardContent}>
          <Typography className={styles.price}>
            {formatPrice(card.price)}
          </Typography>
          <Typography className={styles.title}>
            {truncateTitle(card.title)}
          </Typography>
          {/* TODO: Реализовать price как отдельная features */}
          <Typography className={styles.rating}>
            ★{card.rating.toFixed(1)}
          </Typography>
        </CardContent>
      </CardActionArea>
    </Card>
  );
};
