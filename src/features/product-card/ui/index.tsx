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
import { Link } from "react-router-dom";
import { Transliterate } from "../../../features/transliter";

type Props = {
  id: string;
  image: string;
  title: string;
  price: number;
  rating: number;
};

export const ProductCard = ({ id, image, title, price, rating }: Props) => {
  const productUrl = `/product/${Transliterate(title)}/?id=${id}`;

  return (
    <Card className={styles.card}>
      <Link
        to={productUrl}
        style={{ textDecoration: "none" }}
        className={styles.link}>
        <CardActionArea>
          <CardMedia
            component="img"
            image={image}
            alt={title}
            className={styles.image}
          />
          <CardContent>
            <Typography variant="h6" component="div" className={styles.title}>
              {title}
            </Typography>
            <Typography variant="h5" className={styles.price}>
              ${price.toFixed(0)}
            </Typography>
            <Rating
              name="read-only"
              value={rating}
              precision={0.5}
              readOnly
              emptyIcon={<StarIcon style={{ opacity: 1 }} fontSize="inherit" />}
              className={styles.rating}
            />
          </CardContent>
        </CardActionArea>
      </Link>
    </Card>
  );
};
