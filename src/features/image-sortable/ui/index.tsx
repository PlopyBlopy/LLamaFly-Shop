import React, { useState } from "react";
import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { Box, IconButton } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import Zoom from "@mui/material/Zoom";
import styles from "./index.module.css";

export const SortableItem = ({
  id,
  preview,
  onRemove,
}: {
  id: number;
  preview: string;
  onRemove: () => void;
}) => {
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({ id });
  const [isHovered, setIsHovered] = useState(false);
  const [position, setPosition] = useState({ x: 0, y: 0 });

  const style = {
    transform: CSS.Translate.toString(transform),
    transition,
    touchAction: "none",
  };

  const handleMouseMove = (e: React.MouseEvent) => {
    const viewportWidth = window.innerWidth;
    const viewportHeight = window.innerHeight;
    const offset = 15;

    let x = e.clientX + offset;
    let y = e.clientY + offset;

    if (x + 500 > viewportWidth) x = viewportWidth - 500 - offset;
    if (y + 500 > viewportHeight) y = viewportHeight - 500 - offset;

    setPosition({ x, y });
  };

  return (
    <Box
      ref={setNodeRef}
      style={style}
      className={styles.container}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}>
      <Box
        {...attributes}
        {...listeners}
        onMouseMove={handleMouseMove}
        className={styles.dragHandle}>
        <img
          src={preview}
          alt={`Preview ${id}`}
          className={styles.previewImage}
        />
      </Box>

      <IconButton
        className={styles.deleteButton}
        onClick={(e) => {
          e.stopPropagation();
          onRemove();
        }}
        color="error">
        <DeleteIcon fontSize="small" />
      </IconButton>

      <Zoom in={isHovered}>
        <Box
          className={styles.previewHover}
          style={{
            left: position.x,
            top: position.y,
          }}>
          <img src={preview} alt="Full preview" />
        </Box>
      </Zoom>
    </Box>
  );
};
