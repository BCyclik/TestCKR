using System.Collections.Generic;

public class DogListResponse
{
    public List<Breed> Data { get; set; }
}
public class DogListSingleResponse
{
    public Breed Data { get; set; }
}
public class Breed
{
    public string Id { get; set; }
    public string Type { get; set; }
    public Attributes Attributes { get; set; }
    public Relationships Relationships { get; set; }
}

public class Attributes
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Life Life { get; set; }
    public Weight MaleWeight { get; set; }
    public Weight FemaleWeight { get; set; }
    public bool Hypoallergenic { get; set; }
}

public class Life
{
    public int Max { get; set; }
    public int Min { get; set; }
}

public class Weight
{
    public int Max { get; set; }
    public int Min { get; set; }
}

public class Relationships
{
    public RelationshipData Group { get; set; }
}

public class RelationshipData
{
    public DataInfo Data { get; set; }
}

public class DataInfo
{
    public string Id { get; set; }
    public string Type { get; set; }
}
