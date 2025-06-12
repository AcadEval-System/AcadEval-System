namespace AcadEvalSys.Domain.Exceptions;

public class DuplicateResourceException(string resourceType, string resourceIdentifier) : 
    Exception($"{resourceType} with id: {resourceIdentifier} not found")
{
    
}