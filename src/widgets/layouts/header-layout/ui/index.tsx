import { Toolbar, Box, Container } from "@mui/material";
import { CategoryComponent } from "../../../category-menu";
import { observer } from "mobx-react-lite";
import styles from "./index.module.css";
import { SearchComponent } from "../../../searchbar";
import { LogoRouterComponent } from "../../../logo-button-router";
import { ProfileComponent } from "../../../profile-dropdown-menu";

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

        <Box>
          <ProfileComponent />
        </Box>
      </Toolbar>
    </Container>
  );
});
