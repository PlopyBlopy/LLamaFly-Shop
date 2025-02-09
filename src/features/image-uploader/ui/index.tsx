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

interface ImageUploaderProps {
  onImagesChange: (images: string[]) => void;
}

export const ImageUploader: React.FC<ImageUploaderProps> = ({
  onImagesChange,
}) => {
  const [previews, setPreviews] = useState<string[]>([]);
  const [activeId, setActiveId] = useState<string | null>(null);

  const sensors = useSensors(
    useSensor(PointerSensor),
    useSensor(KeyboardSensor)
  );

  const onDrop = useCallback(
    (acceptedFiles: File[]) => {
      const newPreviews = acceptedFiles.map((file) =>
        URL.createObjectURL(file)
      );
      setPreviews((prev) => [...prev, ...newPreviews]);
      onImagesChange([...previews, ...newPreviews]);
    },
    [previews, onImagesChange]
  );

  const { getRootProps, getInputProps, isDragActive } = useDropzone({
    onDrop,
    accept: { "image/*": [".jpeg", ".jpg", ".png", ".gif"] },
  });

  const handleDragStart = (event: any) => {
    setActiveId(event.active.id);
  };

  const handleDragEnd = (event: any) => {
    const { active, over } = event;

    if (active.id !== over.id) {
      setPreviews((items) => {
        const oldIndex = items.findIndex((item) => item === active.id);
        const newIndex = items.findIndex((item) => item === over.id);
        const newItems = arrayMove(items, oldIndex, newIndex);
        onImagesChange(newItems);
        return newItems;
      });
    }

    setActiveId(null);
  };

  const handleRemove = (index: number) => {
    const newPreviews = previews.filter((_, i) => i !== index);
    setPreviews(newPreviews);
    onImagesChange(newPreviews);
  };

  return (
    <Box>
      <DndContext
        sensors={sensors}
        modifiers={[restrictToWindowEdges]}
        collisionDetection={closestCenter}
        onDragStart={handleDragStart}
        onDragEnd={handleDragEnd}>
        <SortableContext items={previews} strategy={rectSortingStrategy}>
          <Grid container spacing={2} sx={{ mb: 2 }}>
            {previews.map((preview, index) => (
              <Grid item xs={6} sm={4} md={3} key={preview}>
                <SortableItem
                  id={preview}
                  preview={preview}
                  onRemove={() => handleRemove(index)}
                />
              </Grid>
            ))}
          </Grid>
        </SortableContext>

        <DragOverlay>
          {activeId ? (
            <img
              src={activeId}
              alt=""
              style={{
                width: 150,
                height: 150,
                objectFit: "cover",
                borderRadius: 4,
                boxShadow: "0px 5px 15px rgba(0,0,0,0.2)",
              }}
            />
          ) : null}
        </DragOverlay>
      </DndContext>

      <Paper
        variant="outlined"
        sx={{
          p: 2,
          border: "2px dashed #aaa",
          backgroundColor: isDragActive ? "action.hover" : "background.paper",
          cursor: "pointer",
        }}
        {...getRootProps()}>
        <input {...getInputProps()} />
        <Box
          sx={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            gap: 1,
          }}>
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
