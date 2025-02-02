// import {
//   Smartphone as SmartphoneIcon,
//   Tv as TvIcon,
//   Male as MaleIcon,
//   Female as FemaleIcon,
//   ChildFriendly as ChildFriendlyIcon,
//   Chair as ChairIcon,
//   Bed as BedIcon,
//   Lightbulb as LightbulbIcon,
//   CheckBoxOutlineBlank as CheckBoxOutlineBlankIcon,
// } from "@mui/icons-material";

import KitchenIcon from "@mui/icons-material/Kitchen"; // Бытовая техника
import TvIcon from "@mui/icons-material/Tv"; // Электроника
import CheckroomIcon from "@mui/icons-material/Checkroom"; // Одежда
import DirectionsRunIcon from "@mui/icons-material/DirectionsRun"; // Обувь
import LocalLaundryServiceIcon from "@mui/icons-material/LocalLaundryService"; // Крупная бытовая техника
import MicrowaveIcon from "@mui/icons-material/Microwave"; // Кухонная техника
import HeadphonesIcon from "@mui/icons-material/Headphones"; // Наушники
import SmartphoneIcon from "@mui/icons-material/Smartphone"; // Смартфоны
import CameraAltIcon from "@mui/icons-material/CameraAlt"; // Фото и видеокамеры
import WomanIcon from "@mui/icons-material/Woman"; // Женщинам
import ManIcon from "@mui/icons-material/Man"; // Мужчинам
import CheckBoxOutlineBlankIcon from "@mui/icons-material/CheckBoxOutlineBlank"; // Иконка по умолчанию

export interface CategoryConfig {
  id: string;
  title: string;
  icon: React.ReactNode;
}

export const defaultIcon = <CheckBoxOutlineBlankIcon />;

export const categoryIcons: CategoryConfig[] = [
  {
    id: "084b97e6-dfc5-4f19-a7e7-852eececfa04",
    title: "Бытовая техника",
    icon: <KitchenIcon />,
  },
  {
    id: "8bdb90e4-c1bb-4b0e-bfd9-6052795ff8a2",
    title: "Крупная бытовая техника",
    icon: <LocalLaundryServiceIcon />,
  },
  {
    id: "f688e328-5362-4fd4-9fab-1e82e14da64e",
    title: "Кухонная техника",
    icon: <MicrowaveIcon />,
  },
  {
    id: "823273f7-6647-4a4a-abb4-f0841f2e0152",
    title: "Электроника",
    icon: <TvIcon />,
  },
  {
    id: "522490c6-dbf9-4f01-a727-5ecb401ba352",
    title: "Телевизоры",
    icon: <TvIcon />,
  },
  {
    id: "7a0363bf-f4b0-4ddf-9904-1e601d21f79b",
    title: "Наушники",
    icon: <HeadphonesIcon />,
  },
  {
    id: "b9f44d5d-4f64-49e9-8f97-3c9b5585b14c",
    title: "Смартфоны",
    icon: <SmartphoneIcon />,
  },
  {
    id: "d68a1ee4-80d6-4d00-ad69-8177c5365597",
    title: "Фото и видеокамеры",
    icon: <CameraAltIcon />,
  },
  {
    id: "abe131cf-ff1f-4013-8f64-cd739c01aab1",
    title: "Одежда",
    icon: <CheckroomIcon />,
  },
  {
    id: "392beb64-843e-4906-aea0-8f6155b1d648",
    title: "Женщинам",
    icon: <WomanIcon />,
  },
  {
    id: "0d6509d3-232e-4da2-a235-5490e7a98238",
    title: "Верхняя одежда",
    icon: <CheckroomIcon />,
  },
  {
    id: "6104f36c-7b13-45cf-b048-9125e84df38b",
    title: "Нижняя одежда",
    icon: <CheckroomIcon />,
  },
  {
    id: "7e365ce9-f5fc-49af-8784-409500d642e0",
    title: "Мужчинам",
    icon: <ManIcon />,
  },
  {
    id: "c8975c3c-8721-4d4b-b674-5e8b629ecd0a",
    title: "Нижняя одежда",
    icon: <CheckroomIcon />,
  },
  {
    id: "36dfb3f1-8777-4dca-b562-957b1a5ca445",
    title: "Верхняя одежда",
    icon: <CheckroomIcon />,
  },
  {
    id: "df1c1861-34ac-4669-8032-c482cd34d7e5",
    title: "Обувь",
    icon: <DirectionsRunIcon />,
  },
  {
    id: "5ca9ffd4-e189-4fae-b7d6-cf51b1120b7d",
    title: "Мужчинам",
    icon: <ManIcon />,
  },
  {
    id: "85950bc8-7951-4bf0-9dcc-b85c71c605c3",
    title: "Кроссовки",
    icon: <DirectionsRunIcon />,
  },
  {
    id: "026b43a5-8c91-4154-8da3-f75ed19810f4",
    title: "Ботинки",
    icon: <DirectionsRunIcon />,
  },
  {
    id: "e63ab68e-68ac-468f-8e41-00ea0aaa1e7e",
    title: "Женщинам",
    icon: <WomanIcon />,
  },
  {
    id: "0d2f8433-0cba-45d1-8c89-5e5f4349a6d5",
    title: "Кеды",
    icon: <DirectionsRunIcon />,
  },
  {
    id: "11515f97-67c8-491a-9bb7-ae3e65becc70",
    title: "Туфли",
    icon: <DirectionsRunIcon />,
  },
];
