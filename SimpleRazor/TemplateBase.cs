using System.Text;
using SimpleRazor.Attributes;

namespace SimpleRazor
{
    /// <summary>
    ///     Represents a template and model.
    /// </summary>
    /// <typeparam name="T">The template model type.</typeparam>
    public abstract class TemplateBase<T>
    {
        private StringBuilder builder;

        /// <summary>
        ///     Gets or sets the model.
        /// </summary>
        /// <value>
        ///     The model.
        /// </value>
        public T Model { get; set; }

        /// <summary>
        ///     Executes the generated template code.
        /// </summary>
        public abstract void Execute();

        /// <summary>
        ///     Writes the specified value to the template.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void Write(dynamic value)
        {
            this.WriteLiteral(value);
        }

        /// <summary>
        ///     Writes the specified value to the template.
        /// </summary>
        /// <param name="value">The value to write.</param>
        public void WriteLiteral(dynamic value)
        {
            this.builder.Append(value);
        }

        /// WriteAttribute implementation lifted from ANurse's MicroRazor Implementation and the AspWebStack source
        /// <summary>
        ///     Writes the specified attribute to the template.
        /// </summary>
        /// <param name="name">The attribute name.</param>
        /// <param name="prefix">The attribute prefix.</param>
        /// <param name="suffix">The attribute suffix.</param>
        /// <param name="values">The attribute values.</param>
        public virtual void WriteAttribute(string name, PositionTagged<string> prefix, PositionTagged<string> suffix, params AttributeValue[] values)
        {
            var first = true;
            var wroteSomething = false;
            if (values.Length == 0)
            {
                // Explicitly empty attribute, so write the prefix and suffix
                this.WritePositionTaggedLiteral(prefix);
                this.WritePositionTaggedLiteral(suffix);
            }
            else
            {
                for (var i = 0; i < values.Length; i++)
                {
                    var attrVal = values[i];
                    var val = attrVal.Value;

                    bool? boolVal = null;
                    if (val.Value is bool)
                    {
                        boolVal = (bool) val.Value;
                    }

                    if (val.Value != null && (boolVal == null || boolVal.Value))
                    {
                        var valStr = val.Value as string;

                        if (valStr == null)
                        {
                            valStr = val.Value.ToString();
                        }

                        if (boolVal != null)
                        {
                            valStr = name;
                        }

                        if (first)
                        {
                            this.WritePositionTaggedLiteral(prefix);
                            first = false;
                        }
                        else
                        {
                            this.WritePositionTaggedLiteral(attrVal.Prefix);
                        }

                        if (attrVal.Literal)
                        {
                            this.WriteLiteral(valStr);
                        }
                        else
                        {
                            this.Write(valStr);
                        }

                        wroteSomething = true;
                    }
                }

                if (wroteSomething) this.WritePositionTaggedLiteral(suffix);
            }
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            this.builder = new StringBuilder();
            this.Execute();
            return this.builder.ToString();
        }

        private void WritePositionTaggedLiteral(PositionTagged<string> value)
        {
            if (value.Value == null) return;
            this.WriteLiteral(value.Value);
        }
    }
}
