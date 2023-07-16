import { motion } from "framer-motion";

const loaderVariants = {
  initial: {
    opacity: 0,
    y: 500,
  },
  animate: {
    opacity: 1,
    y: 0,
    scale: 5,
    transition: {
      y: { ease: [0.6, 0.01, -0.05, 0.95], duration: 0.8 },
      scale: { delay: 0.8, duration: 0.8 },
    },
  },
};

export const Loader = ({ onChangeLoader }: { onChangeLoader: (finished: boolean) => void }) => {
  return (
    <motion.div
      variants={loaderVariants}
      initial={"initial"}
      animate={"animate"}
      exit={"exit"}
      className="flex items-center justify-center h-[calc(100vh-80px)]"
      onAnimationComplete={() => onChangeLoader(false)}
      layoutId="load-animation"
    >
      <div className="w-96 h-96 rounded-full bg-gradient-radial from-green-400 to-cyan-400 filter blur-3xl opacity-90 -z-10"></div>
    </motion.div>
  );
};
