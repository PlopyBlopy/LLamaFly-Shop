import CategoryIcon from "@mui/icons-material/Category";
import { useEffect, useState } from "react";
import {
  Drawer,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Box,
  Button,
  ListItemIcon,
  Grid,
  Typography,
} from "@mui/material";
import { Category } from "../../../shared/product-service-categories";
import { categoryIcons, defaultIcon } from "../config";
import { LoadingCircle } from "../../loading-circle";
import styles from "./index.module.css";

type Props = {
  categoryList: Category[];
  isLoading: boolean;
  onCategoryShow: () => void;
  // eslint-disable-next-line
  onCategorySelect: (categoryId: string) => void;
};

export const CategoryDrawer = ({
  categoryList,
  isLoading,
  onCategoryShow,
  onCategorySelect,
}: Props) => {
  const [open, setOpen] = useState(false); // Состояние для открытия/закрытия Drawer
  const [selectedCategory, setSelectedCategory] = useState<string | null>(null); //categoriesArray[0].id

  // Устанавливаем selectedCategory при изменении categoryList или открытии Drawer
  useEffect(() => {
    if (open && categoryList.length > 0) {
      setSelectedCategory(categoryList[0].id);
    }
  }, [open, categoryList]);

  const handleCategorySelect = (categoryId: string) => {
    setOpen(false);
    onCategorySelect(categoryId);
  };

  // Обработчик открытия/закрытия Drawer
  const toggleDrawer = (isOpen: boolean) => () => {
    setOpen(isOpen);
    if (isOpen) {
      onCategoryShow();
    }
  };

  const getCategoryIcon = (categoryId: string): React.ReactNode => {
    const categoryConfig = categoryIcons.find(
      (config) => config.id === categoryId
    );
    return categoryConfig ? categoryConfig.icon : defaultIcon;
  };

  // Обработчик наведения на категорию
  const handleCategoryHover = (categoryId: string) => {
    setSelectedCategory(categoryId);
  };

  // Получение данных для выбранной категории 1 уровня
  const categorySelectedData = categoryList.find(
    (category) => category.id === selectedCategory
  );

  return (
    <div>
      {/* Кнопка для открытия Drawer */}
      <Button
        startIcon={<CategoryIcon />}
        variant="contained"
        onClick={toggleDrawer(true)}>
        Категории
      </Button>

      {/* Сам Drawer */}
      <Drawer anchor="left" open={open} onClose={toggleDrawer(false)}>
        <Box className={styles.drawerContainer}>
          {isLoading ? (
            <LoadingCircle />
          ) : (
            <>
              {/* Левая колонка: Категории 1 уровня */}
              <Box className={styles.leftColumn}>
                <List className={styles.list}>
                  {categoryList.map((category) => (
                    <ListItem key={category.id} disablePadding>
                      <ListItemButton
                        onMouseEnter={() => handleCategoryHover(category.id)}
                        onClick={() => handleCategorySelect(category.id)}
                        selected={selectedCategory === category.id}>
                        <ListItemIcon>
                          {getCategoryIcon(category.id)}
                        </ListItemIcon>
                        <ListItemText primary={category.title} />
                      </ListItemButton>
                    </ListItem>
                  ))}
                </List>
              </Box>

              {/* Правая колонка: Категории 2 и 3 уровня */}
              <Box className={styles.rightColumn}>
                {categorySelectedData ? (
                  <Grid container spacing={2}>
                    {categorySelectedData.subcategories.map((subCategory) => (
                      <Grid item xs={6} key={subCategory.id}>
                        <Button
                          startIcon={getCategoryIcon(subCategory.id)}
                          className={styles.customButton}
                          onClick={() => handleCategorySelect(subCategory.id)}>
                          {subCategory.title}
                        </Button>
                        <List className={styles.subList}>
                          {subCategory.subcategories.map((subSubCategory) => (
                            <ListItem key={subSubCategory.id} disablePadding>
                              <Button
                                className={styles.customButton}
                                startIcon={getCategoryIcon(subSubCategory.id)}
                                onClick={() =>
                                  handleCategorySelect(subSubCategory.id)
                                }>
                                {subSubCategory.title}
                              </Button>
                            </ListItem>
                          ))}
                        </List>
                      </Grid>
                    ))}
                  </Grid>
                ) : (
                  <Box className={styles.centeredText}>
                    <Typography variant="h6">Выберите категорию</Typography>
                  </Box>
                )}
              </Box>
            </>
          )}
        </Box>
      </Drawer>
    </div>
  );
};
