import React, { useState } from "react";
import { useSortable } from "@dnd-kit/sortable";
import { CSS } from "@dnd-kit/utilities";
import { Box, IconButton } from "@mui/material";
import DeleteIcon from "@mui/icons-material/Delete";
import Zoom from "@mui/material/Zoom";

export const SortableItem = ({
  id,
  preview,
  onRemove,
}: {
  id: string;
  preview: string;
  onRemove: () => void;
}) => {
  const { attributes, listeners, setNodeRef, transform, transition } =
    useSortable({ id });
  const [isHovered, setIsHovered] = useState(false);
  const [position, setPosition] = useState({ x: 0, y: 0 });

  const style = {
    transform: CSS.Transform.toString(transform),
    transition,
  };

  const handleMouseMove = (e: React.MouseEvent) => {
    const viewportWidth = window.innerWidth;
    const viewportHeight = window.innerHeight;
    const offset = 15;

    let x = e.clientX + offset;
    let y = e.clientY + offset;

    // Проверка правой границы
    if (x + 500 > viewportWidth) {
      x = viewportWidth - 500 - offset;
    }

    // Проверка нижней границы
    if (y + 500 > viewportHeight) {
      y = viewportHeight - 500 - offset;
    }

    setPosition({ x, y });
  };

  return (
    <Box
      ref={setNodeRef}
      style={style}
      sx={{
        position: "relative",
        cursor: "grab",
        "&:active": {
          cursor: "grabbing",
        },
      }}
      {...attributes}
      {...listeners}
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
      onMouseMove={handleMouseMove}>
      <img
        src={preview}
        alt={`Preview ${id}`}
        style={{
          width: "100%",
          height: 150,
          objectFit: "cover",
          borderRadius: 4,
          pointerEvents: "none",
        }}
      />

      {/* Кнопка удаления */}
      <IconButton
        sx={{
          position: "absolute",
          top: 4,
          right: 4,
          color: "error.main",
          backgroundColor: "background.paper",
        }}
        onClick={(e) => {
          e.stopPropagation();
          onRemove();
        }}>
        <DeleteIcon fontSize="small" />
      </IconButton>

      {/* Превью при наведении */}
      <Zoom in={isHovered}>
        <Box
          sx={{
            position: "fixed",
            left: position.x,
            top: position.y,
            zIndex: 9999,
            pointerEvents: "none",
            boxShadow: 3,
            borderRadius: 1,
            overflow: "hidden",
            maxWidth: 500,
            maxHeight: 500,
            bgcolor: "background.paper",
          }}>
          <img
            src={preview}
            alt="Full preview"
            style={{
              maxWidth: "100%",
              maxHeight: "100%",
              objectFit: "contain",
            }}
          />
        </Box>
      </Zoom>
    </Box>
  );
};
