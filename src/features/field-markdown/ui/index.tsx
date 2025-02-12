import { useState } from "react";
import ReactMarkdown from "react-markdown";
import TextField from "@mui/material/TextField";
import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";
import Typography from "@mui/material/Typography";
import remarkGfm from "remark-gfm";

export const MarkdownEditor = () => {
  const [markdown, setMarkdown] = useState("");

  return (
    <Box sx={{ display: "flex", gap: 3, flexDirection: "column" }}>
      <TextField
        label="Описание товара (Markdown)"
        multiline
        fullWidth
        minRows={10}
        value={markdown}
        onChange={(e) => setMarkdown(e.target.value)}
        variant="outlined"
      />

      <Paper elevation={3} sx={{ p: 2 }}>
        <Typography variant="h6" gutterBottom>
          Предпросмотр:
        </Typography>
        <ReactMarkdown remarkPlugins={[remarkGfm]}>{markdown}</ReactMarkdown>
      </Paper>
    </Box>
  );
};
