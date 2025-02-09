import React, { useState } from "react";
import {
  Button,
  TextField,
  Box,
  Grid,
  FormControl,
  InputLabel,
  MenuItem,
  Select,
  SelectChangeEvent,
} from "@mui/material";
import { ProductForm } from "..";
import { ImageUploader } from "../../image-uploader";

interface ProductFormProps {
  onSubmit: (formData: ProductForm) => void;
  categories: string[];
  sellers: string[];
}

export const ProductAddForm: React.FC<ProductFormProps> = ({
  onSubmit,
  categories,
  sellers,
}) => {
  const [formData, setFormData] = useState<Omit<ProductForm, "images">>({
    title: "",
    description: "",
    price: 0,
    categoryId: "",
    sellerId: "",
  });
  const [images, setImages] = useState<string[]>([]);

  const handleChange = (
    e:
      | React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
      | SelectChangeEvent<string>
  ) => {
    const { name, value } = e.target;
    setFormData((prev) => ({
      ...prev,
      [name]: name === "price" ? Number(value) : value,
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

        <Grid item xs={12}>
          <TextField
            fullWidth
            label="Description"
            name="description"
            multiline
            rows={4}
            value={formData.description}
            onChange={handleChange}
            required
          />
        </Grid>

        <Grid item xs={12} sm={6}>
          <FormControl fullWidth required>
            <InputLabel>Category</InputLabel>
            <Select
              name="categoryId"
              value={formData.categoryId}
              label="Category"
              onChange={handleChange}>
              {categories.map((category) => (
                <MenuItem key={category} value={category}>
                  {category}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
        </Grid>

        <Grid item xs={12} sm={6}>
          <FormControl fullWidth required>
            <InputLabel>Seller</InputLabel>
            <Select
              name="sellerId"
              value={formData.sellerId}
              label="Seller"
              onChange={handleChange}>
              {sellers.map((seller) => (
                <MenuItem key={seller} value={seller}>
                  {seller}
                </MenuItem>
              ))}
            </Select>
          </FormControl>
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
