import { Toolbar, Box, IconButton, Badge, Container } from "@mui/material";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import { CategoryComponent } from "../../../category-menu";
import { observer } from "mobx-react-lite";
import styles from "./index.module.css";
import { SearchComponent } from "../../../searchbar";
import { ProfileComponent } from "../../../profile-menu";
import { LogoRouterComponent } from "../../../logo-button-router/ui";

export const HeaderLayout = observer(() => {
  return (
    <Container className={styles.container}>
      <Toolbar>
        <Box className={styles.logo}>
          <LogoRouterComponent />
        </Box>

        <Box className={styles.categoryMenu}>
          <CategoryComponent />
        </Box>

        <Box className={styles.searchBar}>
          <SearchComponent />
        </Box>

        <Box className={styles.icons}>
          <IconButton color="inherit">
            <Badge badgeContent={4} color="error">
              <ShoppingCartIcon />
            </Badge>
          </IconButton>
          <ProfileComponent />
        </Box>
      </Toolbar>
    </Container>
  );
});
