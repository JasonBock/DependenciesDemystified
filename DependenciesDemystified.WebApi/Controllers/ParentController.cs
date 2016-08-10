using DependenciesDemystified.Core.Parents;
using System;
using System.Web.Http;

namespace DependenciesDemystified.WebApi.Controllers
{
	public sealed class ParentController
		: ApiController
	{
		private readonly Lazy<IParent> parent;

		public ParentController(Lazy<IParent> parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException(nameof(parent));
			}

			this.parent = parent;
		}

		public IHttpActionResult Get()
		{
			return this.Ok(this.parent.Value);
		}
	}
}
