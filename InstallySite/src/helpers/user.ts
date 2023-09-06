export function getUserInitials(name: string)
{
  const nameSplited = name?.split(" ");
  const initials = nameSplited[0].slice(0,1) + nameSplited[nameSplited?.length-1].slice(0,1);
  return initials;
}