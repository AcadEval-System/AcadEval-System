namespace AcadEvalSys.Domain.Exceptions;

public class DuplicateResourceException(string resourceType, string resourceIdentifier) : 
    Exception($"{resourceType} with name '{resourceIdentifier}' already exists")
{
    
}