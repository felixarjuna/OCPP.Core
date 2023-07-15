import { motion } from "framer-motion";

interface IMouse {
  r: number; // radius in rem
  x: number; // x position
  y: number; // y position
}

export const Mouse = ({ r, x, y }: IMouse) => {
  return (
    <motion.div
      animate={{ x: x - (r * 16) / 2 - 40, y: y - (r * 16) / 2 - 40 }}
      style={{ width: `${r}rem`, height: `${r}rem` }}
      className={`absolute rounded-full bg-gradient-radial from-green-400 to-cyan-400 filter blur-3xl opacity-90 -z-10`}
    />
  );
};
