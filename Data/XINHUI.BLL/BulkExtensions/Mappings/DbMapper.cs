using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BulkExtensions
{
  internal class DbMapper
  {
    private static readonly Dictionary<Type, DbMapping> _mappings = new Dictionary<Type, DbMapping>();

    public static DbMapping GetDbMapping(DbContext context)
    {
      var contextType = context.GetType();
      if (_mappings.ContainsKey(contextType))
      {
        return _mappings[contextType];
      }

      var mapping = new DbMapping(context);
      _mappings[contextType] = mapping;
      return mapping;
    }
  }
}
