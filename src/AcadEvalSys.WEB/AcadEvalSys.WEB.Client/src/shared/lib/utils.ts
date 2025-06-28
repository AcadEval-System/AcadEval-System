export const nameToSlug = (name: string) =>
  name.toLowerCase().trim().replace(/\s+/g, "-");
