namespace MySpot.Api.Exceptions;

public sealed class EmptyLicensePlateException() : CustomException("License plate is empty."){}
