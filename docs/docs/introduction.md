# Introduction

## [Object Extensions](./object-extensions)

This contains useful extension methods for interacting with objects.

My favorites are `Touch` which lets you alter an object as a monad, and
`NotNull` which throws an exception if the object is null.

## [Linq Extensions](./linq-extensions)

Extensions for interacting with lists, sets and dictionaries.

My favorite is `Safe` which lets you safely dereference an array or list that
might be out of bounds.

## [String Extensions](./string-extensions)

Extensions for working with strings. My favorite is `StringJoin` which converts
a set of strings into one string.

## [Tree Extensions](./tree)

Extension methods for traversing tree.

## [Print Debug](./print-debug)

Allows you to print a debug representation of an object in a way that resembles
rusts RON format.

The `Debug` extension method works for C#'s built in set types.

You can implement the `PrintDebug` interface to customize how `Debug` works for
a particular type. It includes a single method `EnumerateFields` which expects
you to return a tuple of field names with their values. The `Debug` method will
recursively print every value which implements `PrintDebug` and every element in
a collection or dictionary.
