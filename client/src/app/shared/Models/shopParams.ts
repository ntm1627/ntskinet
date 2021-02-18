// possible to use interface but it is made a class to initialize new instances
export class ShopParams{
  brandId = 0;  //this is to make the all option selected in the side menu
  typeId = 0; //this is to make the all option selected in the side menu
  sort='name';
  pageNumber=1; //default page
  pageSize=6 ;//default to be displayed in a single page
  search: string
}