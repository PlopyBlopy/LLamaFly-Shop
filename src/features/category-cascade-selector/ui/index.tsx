import { useState } from "react";
import { FormControl, InputLabel, MenuItem, Select, Box } from "@mui/material";
import { Category } from "../../../shared/services/product-service-categories";

interface CategoryCascadeSelectProps {
  categories: Category[];
  value: string;
  onCategoryChange: (categoryId: string) => void;
}

export const CategoryCascadeSelect = ({
  categories,
  onCategoryChange,
}: CategoryCascadeSelectProps) => {
  const [selectedCategories, setSelectedCategories] = useState<Category[][]>([
    [],
  ]);

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

  const handleCategoryChange = (level: number, categoryId: string) => {
    const selectedCategory = findCategory(categories, categoryId);
    if (!selectedCategory) return;

    const newSelected = selectedCategories.slice(0, level);
    newSelected[level] = [selectedCategory];

    if (selectedCategory.subcategories?.length) {
      newSelected[level + 1] = [];
    }

    setSelectedCategories(newSelected);
    onCategoryChange(selectedCategory.subcategories?.length ? "" : categoryId);
  };

  return (
    <Box>
      {/* <Typography variant="subtitle1" gutterBottom>
        Выбор категории
      </Typography> */}
      <Box display="flex" gap={2} flexWrap="wrap">
        {selectedCategories.map((_, level) => {
          const availableCategories = getAvailableCategories(level);
          return availableCategories.length > 0 ? (
            <FormControl key={level} sx={{ minWidth: 200 }}>
              <InputLabel>{`Уровень ${level + 1}`}</InputLabel>
              <Select
                value={selectedCategories[level]?.[0]?.id || ""}
                onChange={(e) => handleCategoryChange(level, e.target.value)}
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
    </Box>
  );
};
