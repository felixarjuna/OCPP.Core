"use client";

import { Button } from "@/components/ui/button";
import { motion } from "framer-motion";
import { ArrowRight } from "lucide-react";
import Link from "next/link";
import { useRouter } from "next/navigation";

const buttonContainerVariants = {
  animate: {
    y: [200, 0],
    opacity: [0, 1],
    transition: {
      delay: 2.8,
      y: { ease: [0.6, 0.01, -0.05, 0.95], duration: 0.8 },
    },
  },
};

export const ButtonAndLink = () => {
  const router = useRouter();
  return (
    <motion.div
      variants={buttonContainerVariants}
      className="flex gap-8 items-center justify-center"
      initial={"initial"}
      animate={"animate"}
      exit={"exit"}
    >
      <Button variant={"secondary"} onClick={() => router.push("/dashboard")}>
        Get started
      </Button>
      <Link href={""} className="hover:underline hover:underline-offset-4">
        <div className="flex gap-1 items-center">
          <p className="text-sm">Learn more</p>
          <ArrowRight className="w-4 h-4" />
        </div>
      </Link>
    </motion.div>
  );
};
