x = rbinom(n = 1000, size = 6, prob = 0.25)

hist(
  x,
  breaks = seq(min(x) - 0.5, max(x) + 0.5, by = 1),
  freq = FALSE,
  ylim = c(0, 0.44),
  las = 1,
  col = "#1155cc",
  main = "Random function: rbinom(n = 1000, size = 6, prob = 0.25)",
  ylab = "Probability"
)