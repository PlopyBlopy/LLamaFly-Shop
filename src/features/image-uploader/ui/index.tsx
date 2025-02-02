import React, { useState } from "react";
import { Button, TextField, Box, Typography, Input } from "@mui/material";
import { CreateImage } from "../../../shared/image-service-images";
import { uploadImages } from "../../../shared/image-service-images/index";

export const ImageUploader = () => {
  const [productId, setProductId] = useState(
    "3fa85f64-5717-4562-b3fc-2c963f66afa6"
  );
  const [images, setImages] = useState<CreateImage[]>([]);

  // Обработчик изменения productId
  const handleProductIdChange = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setProductId(event.target.value);
  };

  // Обработчик выбора файлов
  const handleFileChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event.target.files) {
      const files = Array.from(event.target.files);
      const newImages = files.map((file, index) => ({
        order: index + 1, // Порядковый номер начинается с 1
        image: file,
      }));
      setImages(newImages);
    }
  };

  // Обработчик изменения порядка
  const handleOrderChange = (index: number, order: number) => {
    const updatedImages = [...images];
    updatedImages[index].order = order;
    setImages(updatedImages);
  };

  // Обработчик отправки данных
  const handleSubmit = async () => {
    if (!productId || images.length === 0) {
      alert("Please enter a Product ID and select at least one image.");
      return;
    }

    try {
      await uploadImages({ productId, images });
      alert("Images uploaded successfully!");
    } catch (error) {
      console.error("Upload failed:", error);
      alert("Upload failed. Please try again.");
    }
  };

  return (
    <Box sx={{ maxWidth: 500, margin: "auto", padding: 3 }}>
      <Typography variant="h5" gutterBottom>
        Upload Images
      </Typography>

      {/* Поле для ввода Product ID */}
      <TextField
        label="Product ID"
        value={productId}
        onChange={handleProductIdChange}
        fullWidth
        margin="normal"
        required
      />

      {/* Поле для выбора файлов */}
      <Input
        type="file"
        inputProps={{ multiple: true }}
        onChange={handleFileChange}
        fullWidth
        sx={{ marginBottom: 2 }}
      />

      {/* Список выбранных файлов с полем для порядка */}
      {images.map((image, index) => (
        <Box
          key={index}
          sx={{
            display: "flex",
            alignItems: "center",
            gap: 2,
            marginBottom: 2,
          }}>
          <Typography variant="body1">{image.image.name}</Typography>
          <TextField
            label="Order"
            type="number"
            value={image.order}
            onChange={(e) => handleOrderChange(index, parseInt(e.target.value))}
            sx={{ width: 100 }}
          />
        </Box>
      ))}

      {/* Кнопка отправки */}
      <Button
        variant="contained"
        color="primary"
        onClick={handleSubmit}
        fullWidth>
        Upload Images
      </Button>
    </Box>
  );
};
