import styles from "./index.module.css";
import {
  Card,
  CardActionArea,
  CardContent,
  CardMedia,
  Rating,
  Typography,
} from "@mui/material";
import StarIcon from "@mui/icons-material/Star";
import { Transliterate } from "../../../features/transliter";
import { ProductCard } from "../../../shared/product-service-products";

type Props = {
  card: ProductCard;
  onCardClick: (title: string, id: string) => void;
};

export const ProductItemCard = ({ card, onCardClick }: Props) => {
  return (
    <Card className={styles.card}>
      <CardActionArea
        onClick={() => onCardClick(Transliterate(card.title), card.id)}>
        <CardMedia
          component="img"
          image={card.image}
          alt={card.title}
          className={styles.image}
        />
        <CardContent>
          <Typography variant="h6" component="div" className={styles.title}>
            {card.title}
          </Typography>
          <Typography variant="h5" className={styles.price}>
            ${card.price.toFixed(0)}
          </Typography>
          <Rating
            name="read-only"
            value={card.rating}
            precision={0.5}
            readOnly
            emptyIcon={<StarIcon style={{ opacity: 1 }} fontSize="inherit" />}
            className={styles.rating}
          />
        </CardContent>
      </CardActionArea>
    </Card>
  );
};
