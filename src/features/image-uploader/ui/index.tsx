import React, { useState, useCallback } from "react";
import {
  DndContext,
  DragOverlay,
  closestCenter,
  KeyboardSensor,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import {
  arrayMove,
  SortableContext,
  rectSortingStrategy,
} from "@dnd-kit/sortable";
import { restrictToWindowEdges } from "@dnd-kit/modifiers";
import { Box, Grid, Typography, Paper } from "@mui/material";
import CloudUploadIcon from "@mui/icons-material/CloudUpload";
import { useDropzone } from "react-dropzone";
import { SortableItem } from "../../image-sortable";
import styles from "./index.module.css";
import { Image } from "@/shared/services/image-service-product-images";

interface ImageUploaderProps {
  onImagesChange: (images: Image[]) => void;
}

interface InternalImage {
  dndId: number;
  order: number;
  image: File; // Store the actual File
  preview: string; // Data URL for preview
}

export const ImageUploader: React.FC<ImageUploaderProps> = ({
  onImagesChange,
}) => {
  const [previews, setPreviews] = useState<InternalImage[]>([]);
  const [activeId, setActiveId] = useState<number | null>(null);

  const sensors = useSensors(
    useSensor(PointerSensor),
    useSensor(KeyboardSensor)
  );

  const generateUniqueId = () =>
    Date.now() + Math.floor(Math.random() * 1000000);

  const updateOrder = (items: InternalImage[]) => {
    return items.map((item, index) => ({
      ...item,
      order: index,
    }));
  };

  const onDrop = useCallback(
    (acceptedFiles: File[]) => {
      const newImages: InternalImage[] = acceptedFiles.map((file) => ({
        dndId: generateUniqueId(),
        order: previews.length,
        image: file,
        preview: URL.createObjectURL(file),
      }));

      setPreviews((prev) => {
        const updatedPreviews = updateOrder([...prev, ...newImages]);
        // Pass only necessary data to parent
        onImagesChange(
          updatedPreviews.map(({ preview, dndId, ...rest }) => rest)
        );
        return updatedPreviews;
      });
    },
    [previews.length, onImagesChange]
  );

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: { "image/*": [".jpeg", ".jpg", ".png"] },
  });

  const handleDragStart = (event: any) => {
    setActiveId(event.active.id);
  };

  const handleDragEnd = (event: any) => {
    const { active, over } = event;

    if (over && active.id !== over.id) {
      setPreviews((items) => {
        const oldIndex = items.findIndex((item) => item.dndId === active.id);
        const newIndex = items.findIndex((item) => item.dndId === over.id);
        const newItems = arrayMove(items, oldIndex, newIndex);
        const updatedItems = updateOrder(newItems);
        onImagesChange(updatedItems.map(({ dndId, ...rest }) => rest));
        return updatedItems;
      });
    }

    setActiveId(null);
  };

  const handleRemove = (dndId: number) => {
    setPreviews((prev) => {
      const newPreviews = prev.filter((img) => img.dndId !== dndId);
      const updatedPreviews = updateOrder(newPreviews);
      onImagesChange(updatedPreviews.map(({ dndId, ...rest }) => rest));
      return updatedPreviews;
    });
  };

  return (
    <Box className={styles.container}>
      {/* Секция загрузки изображений */}
      <DndContext
        sensors={sensors}
        modifiers={[restrictToWindowEdges]}
        collisionDetection={closestCenter}
        onDragStart={handleDragStart}
        onDragEnd={handleDragEnd}>
        <SortableContext
          items={previews.map((img) => img.dndId)}
          strategy={rectSortingStrategy}>
          <Grid container spacing={2} className={styles.gridContainer}>
            {previews.map((preview) => (
              <Grid item xs={6} sm={4} md={3} key={preview.dndId}>
                <SortableItem
                  id={preview.dndId}
                  preview={preview.preview}
                  onRemove={() => handleRemove(preview.dndId)}
                />
              </Grid>
            ))}
          </Grid>
        </SortableContext>

        <DragOverlay adjustScale={false} dropAnimation={null}>
          {activeId ? (
            <div className={styles.dragOverlay}>
              <img
                src={previews.find((img) => img.dndId === activeId)?.preview}
                alt=""
                className={styles.dragImage}
              />
            </div>
          ) : null}
        </DragOverlay>
      </DndContext>

      <Paper
        variant="outlined"
        className={`${styles.dropzone} ${
          isDragActive ? styles.dropzoneActive : ""
        }`}
        {...getRootProps()}>
        <input {...getInputProps()} />
        <Box className={styles.uploadContent}>
          <CloudUploadIcon fontSize="large" />
          <Typography variant="body2" color="textSecondary">
            {isDragActive
              ? "Отпустите для загрузки"
              : "Перетащите сюда изображения или кликните для выбора"}
          </Typography>
        </Box>
      </Paper>
    </Box>
  );
};
