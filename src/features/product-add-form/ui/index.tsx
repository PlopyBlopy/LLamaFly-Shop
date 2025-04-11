import React, { useState } from "react";
import {
  Button,
  TextField,
  Box,
  Grid,
  SelectChangeEvent,
  Typography,
} from "@mui/material";
import { ImageUploader } from "../../image-uploader";
import { MarkdownEditor } from "../../field-markdown/ui";
import { CategoryCascadeSelect } from "../../category-cascade-selector";
import { ProductForm } from "../../../shared/forms/product-form";
import { ProductFormProps } from "../config";
import { Image } from "@/shared/services/image-service-product-images";

// type SellerProp = {
//   id: string;
//   name: string;
//   surname: string;
//   patronymic: string;
// };

// type ProductFormProps = {
//   onSubmit: (formData: ProductForm) => void;
//   categories: Category[];
//   seller: SellerProp;
// };

export const ProductAddForm = ({
  onSubmit,
  categories,
  seller,
}: ProductFormProps) => {
  const [formData, setFormData] = useState<Omit<ProductForm, "images">>({
    title: "",
    description: "",
    price: 0,
    categoryId: "",
    sellerId: seller.id,
  });
  const [images, setImages] = useState<Image[]>([]);

  const handleChange = (
    e:
      | React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
      | SelectChangeEvent<string>
  ) => {
    const target = e.target as HTMLInputElement; // Явное приведение типа
    setFormData((prev) => ({
      ...prev,
      [target.name]:
        target.name === "price" ? Number(target.value) : target.value,
    }));
  };

  const handleDescriptionChange = (description: string) => {
    setFormData((prev) => ({
      ...prev,
      description,
    }));
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({ ...formData, images });
  };

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 3 }}>
      <Grid container spacing={2}>
        <Grid item xs={12}>
          <ImageUploader onImagesChange={setImages} />
        </Grid>

        {/* Секция Markdown редактора */}
        <Grid item xs={12}>
          <Typography variant="h6" gutterBottom>
            Описание товара
          </Typography>
          <MarkdownEditor
            onChange={handleDescriptionChange}
            value={formData.description}
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            fullWidth
            label="Title"
            name="title"
            value={formData.title}
            onChange={handleChange}
            required
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <TextField
            fullWidth
            label="Price"
            name="price"
            type="number"
            value={formData.price}
            onChange={handleChange}
            required
            InputProps={{ inputProps: { min: 0 } }}
          />
        </Grid>

        {/* Каскадный выбор категорий */}
        <Grid item xs={12}>
          <Typography variant="subtitle1" gutterBottom>
            Выбор категории
          </Typography>
          <CategoryCascadeSelect
            categories={categories}
            value={formData.categoryId}
            onCategoryChange={(categoryId) =>
              setFormData((prev) => ({ ...prev, categoryId }))
            }
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <Typography variant="h5" component="h2">
            Продавец:{" "}
            <span
              style={{
                backgroundColor: "#ffeb3b",
                color: "#000",
                padding: "2px 6px",
                borderRadius: "4px",
                marginLeft: "4px",
              }}>
              {seller.name +
                " " +
                seller.surname +
                " " +
                seller.patronymic +
                " "}
            </span>
          </Typography>
        </Grid>

        <Grid item xs={12}>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}>
            Submit
          </Button>
        </Grid>
      </Grid>
    </Box>
  );
};
