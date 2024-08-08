namespace crud.Response
{
    public class OfficeResponse1
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<OfficeResponse2> Offices { get; set; }

      //  public List<OfficeResponse> ChildName { get; set; }

    }

}
