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
  Typography,
} from "@mui/material";
import { ProductForm } from "..";
import { ImageUploader } from "../../image-uploader";
import { Image } from "../../../shared/image-service-images";

interface Category {
  id: string;
  title: string;
  subcategories: Category[];
}

interface ProductFormProps {
  onSubmit: (formData: ProductForm) => void;
  categories: Category[];
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
  const [images, setImages] = useState<Image[]>([]);
  const [selectedCategories, setSelectedCategories] = useState<Category[][]>([
    [],
  ]);

  const handleCategoryChange = (level: number, categoryId: string) => {
    const selectedCategory = findCategory(categories, categoryId);
    const newSelected = selectedCategories.slice(0, level);
    newSelected[level] = [selectedCategory];

    if (selectedCategory?.subcategories?.length > 0) {
      newSelected[level + 1] = [];
    }

    setSelectedCategories(newSelected);
    setFormData((prev) => ({
      ...prev,
      categoryId: selectedCategory?.subcategories?.length ? "" : categoryId,
    }));
  };

  const findCategory = (
    categories: Category[],
    id: string
  ): Category | null => {
    for (const category of categories) {
      if (category.id === id) return category;
      const found = findCategory(category.subcategories, id);
      if (found) return found;
    }
    return null;
  };

  const getAvailableCategories = (level: number): Category[] => {
    if (level === 0) return categories;
    return selectedCategories[level - 1][0]?.subcategories || [];
  };

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

        {/* Каскадный выбор категорий */}
        <Grid item xs={12}>
          <Typography variant="subtitle1" gutterBottom>
            Выбор категории
          </Typography>
          <Box display="flex" gap={2} flexWrap="wrap">
            {selectedCategories.map((_, level) => {
              const availableCategories = getAvailableCategories(level);
              return availableCategories.length > 0 ? (
                <FormControl key={level} sx={{ minWidth: 200 }}>
                  <InputLabel>{`Уровень ${level + 1}`}</InputLabel>
                  <Select
                    value={selectedCategories[level]?.[0]?.id || ""}
                    onChange={(e) =>
                      handleCategoryChange(level, e.target.value)
                    }
                    label={`Уровень ${level + 1}`}>
                    {availableCategories.map((category) => (
                      <MenuItem key={category.id} value={category.id}>
                        {category.title}
                      </MenuItem>
                    ))}
                  </Select>
                </FormControl>
              ) : null;
            })}
          </Box>
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
